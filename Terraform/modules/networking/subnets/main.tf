data "aws_availability_zones" "available" {}

//public
resource "aws_subnet" "public" {
  vpc_id                  = var.vpc_id
  count                   = var.az_count
  cidr_block              = cidrsubnet(var.vpc_cidr_block, 8, var.az_count + count.index)
  availability_zone       = data.aws_availability_zones.available.names[count.index]
  map_public_ip_on_launch = true

  tags = {
    Name = "${var.environment}-${data.aws_availability_zones.available.names[count.index]}-public-subnet"
  }
}

resource "aws_route_table" "public" {
  vpc_id = var.vpc_id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = var.internet_gateway_id
  }
  tags = {
    Name = "${var.environment}-public-route-table"
  }
}

resource "aws_route_table_association" "public" {
  count          = var.az_count
  subnet_id      = aws_subnet.public[count.index].id
  route_table_id = aws_route_table.public.id
}

resource "aws_main_route_table_association" "public_main" {
  vpc_id         = var.vpc_id
  route_table_id = aws_route_table.public.id
}

//NAT
resource "aws_eip" "nat_eip" {
  vpc = true
  count = var.az_count

  tags = {
    Name = "${var.environment}-nat-gateway-eip-${count.index}"
  }
}

resource "aws_nat_gateway" "nat" {
  count         = var.az_count
  subnet_id     = aws_subnet.public[count.index].id
  allocation_id = aws_eip.nat_eip[count.index].id

  tags = {
    Name = "${var.environment}-nat-gateway"
  }
}

//private
resource "aws_subnet" "private" {
  count=var.az_count
  vpc_id            = var.vpc_id
  cidr_block        = cidrsubnet(var.vpc_cidr_block, 8, count.index)
  availability_zone = data.aws_availability_zones.available.names[count.index]
  map_public_ip_on_launch = false

  tags = {
    Name = "${var.environment}-private-subnet-${count.index}"
  }
}

resource "aws_route_table" "private" {
  vpc_id = var.vpc_id
  count  = var.az_count

  route {
    cidr_block     = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.nat[count.index].id
  }
  tags = {
    Name = "${var.environment}-private-route-table"
  }
}

resource "aws_route_table_association" "private" {
  count          = var.az_count
  subnet_id      = aws_subnet.private[count.index].id
  route_table_id = aws_route_table.private[count.index].id
}

resource "aws_db_subnet_group" "subnet" {
  name        = "rds-subnet"
  subnet_ids = aws_subnet.private[*].id

  tags = {
     Name = "${var.environment}-rds-subnet"
  }
}