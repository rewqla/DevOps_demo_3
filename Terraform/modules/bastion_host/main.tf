data "aws_key_pair" "ssh" {
    key_name = var.key_name
}

resource "aws_instance" "bastion_host" {
   ami = var.ami
   instance_type = var.instance_type
   subnet_id = var.public_subnet_id
   vpc_security_group_ids = [var.security_group_id]
   key_name = data.aws_key_pair.ssh.key_name
   associate_public_ip_address = true
}