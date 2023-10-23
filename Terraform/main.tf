
module "vpc" {
    source = "./modules/networking/vpc"
    environment = var.environment
    vpc_cidr_block = var.vpc_cidr_block
}

module "subnets" {
    source = "./modules/networking/subnets"
    environment = var.environment
    vpc_id = module.vpc.vpc_id
    internet_gateway_id = module.vpc.internet_gateway_id
}

module "security_groups" {
    source = "./modules/networking/security_groups"
    environment = var.environment
    vpc_id = module.vpc.vpc_id
}

module "rds" {
    source = "./modules/rds"
    security_group_id = module.security_groups.rds_security_group_id
    subnet_group_id = module.subnets.rds_subnet_id
}

module "ecr" {
    source = "./modules/ecr"
}

module "bastion_host" {
    source = "./modules/bastion_host"
    security_group_id = module.security_groups.bastion_host_security_group_id
    public_subnet_id = module.subnets.public_subnet_ids[0]
}