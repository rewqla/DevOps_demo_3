resource "aws_route53_zone" "main" {
  name = var.domain_name
}

resource "aws_route53_record" "dns_record" {
  zone_id = aws_route53_zone.main.zone_id
  name = var.domain_name
  type = "A"

  alias {
    name = var.dns_name
    zone_id = var.lb_zone_id
    evaluate_target_health = true
  }
}

resource "aws_acm_certificate" "alb_certificate" {
  domain_name = var.domain_name
  validation_method = "DNS"
  subject_alternative_names = ["*.${var.domain_name}"]
}

resource "aws_acm_certificate_validation" "alb_certificate" {
  certificate_arn         = aws_acm_certificate.alb_certificate.arn
  validation_record_fqdns = [aws_route53_record.generic_certificate_validation.fqdn]
}

resource "aws_route53_record" "generic_certificate_validation" {
  zone_id = aws_route53_zone.main.id
  name    = tolist(aws_acm_certificate.alb_certificate.domain_validation_options)[0].resource_record_name
  type    = tolist(aws_acm_certificate.alb_certificate.domain_validation_options)[0].resource_record_type
  records = [tolist(aws_acm_certificate.alb_certificate.domain_validation_options)[0].resource_record_value]
  ttl     = 300
} 