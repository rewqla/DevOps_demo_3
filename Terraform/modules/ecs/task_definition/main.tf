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
  family             = "${var.namespace}-ecs-task-definition-${var.environment}"
  execution_role_arn = var.ecs_execution_role_arn
  task_role_arn      = var.ecs_task_iam_role_arn
  
  container_definitions = jsonencode([
    {
      name         = var.service_name
      image        = "${var.ecr_repository_url}:${var.hash}"
      cpu          = var.cpu_units - 10
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
    },
    {
      name      = "datadog-agent" 
      image     = "datadog/agent:latest"
      cpu: 10,
      memory: 256,
      mountPoints:[],  
      essential = false  

      environment = [
        {
          name  = "DD_API_KEY",
          value = local.db_creds.DatadogApi
        },
        {
            name: "ECS_FARGATE",
            value: "true"
        },
        {
            name: "DD_PROCESS_AGENT_ENABLED",
            value: "true"
        },
        {
            name: "DD_DOGSTATSD_NON_LOCAL_TRAFFIC",
            value: "true"
        }
      ]
    }
  ])
}

  #  logConfiguration = {
  #       logDriver = "awslogs"
  #       options   = {
  #         "awslogs-group" = "/ecs/datadog-agent"  
  #         "awslogs-region" = var.region
  #         "awslogs-stream-prefix" = "datadog-ecs"
  #         "awslogs-create-group": "true"
  #       }
  #     }