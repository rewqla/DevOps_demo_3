output "dns_name" {
    value = aws_lb.lb.dns_name
}

output "zone_id" {
    value = aws_lb.lb.zone_id
}

output "service_target_group_arn" {
  value = aws_lb_target_group.target_group.arn
}