
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

module "load_balancer" {
    source = "./modules/networking/load_balancer"
    vpc_id = module.vpc.vpc_id
    environment = var.environment
    subnets = module.subnets.public_subnet_ids
    security_group_id = module.security_groups.load_balancer_security_group_id
    acm_certificate = module.dns.acm_certificate
}

module "dns"{
    source = "./modules/networking/dns"
    lb_dns_name = module.load_balancer.dns_name
    lb_zone_id = module.load_balancer.zone_id
}