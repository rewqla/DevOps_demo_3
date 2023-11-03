variable "namespace" {
  default = "OilShop"
  type = string
}

variable "environment" {
  type    = string
  default = "development"
}

variable "vpc_cidr_block" {
  type = string
  default = "10.0.0.0/16"
}

variable "service_name" {
  default = "OilShop"
  type = string
}

variable "container_port" {
  default = 80
  type = number
}

variable "host_port" {
  default = 8080
  type = number
}

variable "region" {
  default = "eu-north-1"
  type = string
}