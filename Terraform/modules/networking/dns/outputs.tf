output "route53_zone_id" {
    value = aws_route53_zone.main.id
}

output "acm_certificate" {
  value = aws_acm_certificate.alb_certificate
}