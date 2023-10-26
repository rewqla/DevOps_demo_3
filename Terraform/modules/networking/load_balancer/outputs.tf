output "dns_name" {
  value = aws_alb.lb.dns_name
}

output "zone_id" {
  value = aws_alb.lb.zone_id
}

output "service_target_group_arn" {
  value = aws_alb_target_group.target_group.arn
}