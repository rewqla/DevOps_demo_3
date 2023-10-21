variable "secret_name" {
    type = string
    default = "stage/OilShop/Db"
}

/* variable "security_group_db_id" {
    type = string
}

variable "db_subnet_group_id" {
     type = string
} */

variable "instance_class" {
    type = string
    default = "db.t3.micro"
}