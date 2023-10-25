variable "environment" {
}

variable "cluster_id" {
}

variable "task_definition_arn" {
}

variable "ecs_task_desired_count" {
  default     = 1
  type        = number
}

variable "ecs_task_deployment_minimum_healthy_percent" {
  default     = 50
  type        = number
}

variable "ecs_task_deployment_maximum_percent" {
  default     = 100
  type        = number
}

variable "lb_service_target_group_arn" {
}

variable "service_name" {
}

variable "container_port" {
}

variable "desired_count" {
  default     = 1
  type        = number
}

variable "private_subnets" {
}

variable "security_groups_id" {
}