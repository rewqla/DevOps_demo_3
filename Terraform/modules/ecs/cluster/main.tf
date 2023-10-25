resource "aws_ecs_cluster" "default" {
    name = "ecs-cluster-${var.environment}"

    tags = {
        Name = "ecs-cluster-${var.environment}"
    }
}