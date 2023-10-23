resource "aws_route53_zone" "main" {
  name = var.domain_name
}

resource "aws_route53_record" "dns_record" {
  zone_id = aws_route53_zone.main.zone_id
  name = var.domain_name
  type = "A"

  alias {
    name = var.lb_dns_name
    zone_id = var.lb_zone_id
    evaluate_target_health = true
  }
}

resource "aws_acm_certificate" "certificate" {
  domain_name = var.domain_name
  validation_method = "DNS"

  lifecycle {
    create_before_destroy = true
  }
}

resource "aws_route53_record" "validate" {
  allow_overwrite = true
  ttl     = 60
  zone_id = aws_route53_zone.main.id

  name    = tolist(aws_acm_certificate.certificate.domain_validation_options)[0].resource_record_name
  type    = tolist(aws_acm_certificate.certificate.domain_validation_options)[0].resource_record_type
  records = [tolist(aws_acm_certificate.certificate.domain_validation_options)[0].resource_record_value]
} 


resource "aws_acm_certificate_validation" "validation" {
  certificate_arn         = aws_acm_certificate.certificate.arn
  validation_record_fqdns = [aws_route53_record.validate.fqdn]
}