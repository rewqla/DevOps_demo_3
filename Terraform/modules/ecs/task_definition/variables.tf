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
  default     = 100
  type        = number
}

variable "memory" {
  default     = 256
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