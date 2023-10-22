
module "vpc" {
    source = "./modules/networking/vpc"
    environment = var.enviroment
    vpc_cidr_block = var.vpc_cidr_block
}

module "subnets" {
    source = "./modules/networking/subnets"
    environment = var.enviroment
    vpc_id = module.vpc.vpc_id
    internet_gateway_id = module.vpc.internet_gateway_id
}

module "security_groups" {
    source = "./modules/networking/security_groups"
    vpc_id = module.vpc.vpc_id
}

module "rds" {
    source = "./modules/rds"
    security_group_id = module.security_groups.rds_security_group_id
    subnet_group_id = module.subnets.rds_subnet_id
}