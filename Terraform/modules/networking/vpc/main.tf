resource "aws_vpc" "main" {
    cidr_block           = var.vpc_cidr_block
    enable_dns_support   = true
    enable_dns_hostnames = true
  
    tags = {
        Name = "${var.environment}-vpc"
    }
}

resource "aws_internet_gateway" "main" {
  vpc_id = aws_vpc.main.id

  tags = {
      Name = "${var.environment}-internet-gateway"
  }
}