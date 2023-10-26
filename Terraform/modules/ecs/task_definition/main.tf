data "aws_secretsmanager_secret_version" "creds" {
    secret_id = var.secret_name
}

locals {
    db_creds = jsondecode(
        data.aws_secretsmanager_secret_version.creds.secret_string
    )
}

resource "aws_ecs_task_definition" "default" {
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu          = var.cpu_units
  memory       = var.memory
  family                   = "ecs-task-definition${var.environment}"
  execution_role_arn = var.ecs_execution_role_arn
  task_role_arn      = var.ecs_task_iam_role_arn
  
  container_definitions = jsonencode([
    {
      name         = var.service_name
      image        = "${var.ecr_repository_url}:${var.hash}"
      cpu          = var.cpu_units
      memory       = var.memory
      essential    = true
      portMappings = [
        {
          containerPort = var.container_port
          hostPort      = var.container_port
          protocol      = "tcp"
        }
      ]
       environment : [
        {
          name : "DbName",
          value : local.db_creds.DbName
        },
        {
          name : "DbUsername",
          value : local.db_creds.DbUsername
        },
        {
          name : "DbHost",
          value : var.db_host
        },
        {
          name : "DbPassword",
          value : local.db_creds.DbPassword
        }
      ]
    }
  ])
}