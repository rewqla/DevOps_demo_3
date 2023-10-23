variable "key_name" {
  default = "keyyer"
}

variable "instance_type" {
  default = "t3.micro"
}
variable "ami" {
  default = "ami-0d2ca4d7e5645e504"
}

variable "public_subnet_id" {
  type = string
}

variable "security_group_id" {
  type = string
}