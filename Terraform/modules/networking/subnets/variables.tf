variable "environment" {
    type = string
}

variable "vpc_id" {
    type = string
}

variable "internet_gateway_id" {
    type = string
}

variable "public_subnet_cidrs" {
  type = list(string)
  default = [
    "10.0.1.0/24",
    "10.0.3.0/24"
  ]
}

variable "private_subnet_cidrs" {
  type = list(string)
  default = [
    "10.0.2.0/24",
    "10.0.4.0/24"
  ]
}