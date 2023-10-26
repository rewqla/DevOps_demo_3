variable "container_port" {
}

variable "host_port" {
}

variable "environment" {
}

variable "secret_name" {
  default = "stage/OilShop/Db"
  type = string
}

variable "db_host" {
}

variable "cpu_units" {
  default     = 256
  type        = number
}

variable "memory" {
  default     = 512
  type        = number
}

variable "service_name" {

}

variable "hash" {
  default = "latest"
  type        = string
}

variable "ecr_repository_url" {
  
}

variable "ecs_execution_role_arn" {
  
}

variable "ecs_task_iam_role_arn" {
  
}