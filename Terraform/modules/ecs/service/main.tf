resource "aws_ecs_service" "service" {
  name                               = "ecs-service-${var.environment}"
  cluster                            = var.cluster_id
  task_definition                    = var.task_definition_arn
  desired_count                      = var.ecs_task_desired_count
  deployment_minimum_healthy_percent = var.ecs_task_deployment_minimum_healthy_percent
  deployment_maximum_percent         = var.ecs_task_deployment_maximum_percent
  launch_type                        = "FARGATE"

  load_balancer {
    target_group_arn = var.lb_service_target_group_arn
    container_name   = var.service_name
    container_port   = var.container_port
  }

  network_configuration {
    security_groups  =  [var.security_groups_id]
    subnets          =  var.private_subnets
  }

  lifecycle {
    ignore_changes = [desired_count]
  }
}