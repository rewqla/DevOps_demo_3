output "rds_security_group_id" {
  value = aws_security_group.rds_security_group.id
}

output "bastion_host_security_group_id" {
  value = aws_security_group.bastion_host.id
}