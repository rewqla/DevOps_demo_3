module "vpc" {
    source = "./modules/networking/vpc"
    namespace = var.namespace
    environment = var.environment
    vpc_cidr_block = var.vpc_cidr_block
}

module "subnets" {
    source = "./modules/networking/subnets"
    namespace = var.namespace
    environment = var.environment
    vpc_id = module.vpc.vpc_id
    internet_gateway_id = module.vpc.internet_gateway_id
    vpc_cidr_block=var.vpc_cidr_block
}

module "security_groups" {
    source = "./modules/networking/security_groups"
    namespace = var.namespace
    environment = var.environment
    vpc_id = module.vpc.vpc_id
    container_port = var.container_port
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
    namespace = var.namespace
    vpc_id = module.vpc.vpc_id
    environment = var.environment
    subnets = module.subnets.public_subnet_ids
    security_group_id = module.security_groups.load_balancer_security_group_id
    acm_certificate = module.dns.acm_certificate
}

module "dns"{
    source = "./modules/networking/dns"
    lb_zone_id = module.load_balancer.zone_id
    dns_name = module.load_balancer.dns_name
}

module "ecs_cluster"{
    source = "./modules/ecs/cluster"
    namespace = var.namespace
    environment = var.environment
}

module "iam_role" {
    source ="./modules/ecs/iam_role"
    namespace = var.namespace
    environment = var.environment
}

module "cloud_watch"{
    source = "./modules/ecs/cloud_watch"
    namespace = var.namespace
    service_name = var.service_name
}

module "ecs_service" {
    source = "./modules/ecs/service"
    namespace = var.namespace
    environment = var.environment
    cluster_id = module.ecs_cluster.ecs_cluster_id
    task_definition_arn = module.ecs_task_definition.arn
    security_groups_id = module.security_groups.ecs_security_group_id
    private_subnets = module.subnets.private_subnet_ids
    service_name=var.service_name
    container_port = var.container_port
    lb_service_target_group_arn=module.load_balancer.service_target_group_arn
}

module "ecs_task_definition" {
    source = "./modules/ecs/task_definition"
    namespace = var.namespace
    environment = var.environment
    region=var.region
    db_host = module.rds.db_instance_endpoint
    ecr_repository_url = module.ecr.repository_url
    service_name=var.service_name
    host_port = var.host_port
    log_group_name = module.cloud_watch.log_group_name
    container_port = var.container_port
    ecs_execution_role_arn = module.iam_role.ecs_execution_role_arn
    ecs_task_iam_role_arn=module.iam_role.ecs_task_iam_role_arn
}