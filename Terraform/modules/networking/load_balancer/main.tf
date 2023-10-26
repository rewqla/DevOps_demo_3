resource "aws_alb" "lb" {
  name = "${var.environment}-load-balancer"
  security_groups = [var.security_group_id]
  subnets = var.subnets
}

resource "aws_alb_target_group" "target_group" {
  name                 = "${var.environment}-target-group"
  port                 = 80
  protocol             = "HTTP"
  vpc_id               = var.vpc_id
  deregistration_delay = 5
  target_type = "ip"

  health_check {
    path                = "/"
    matcher             = "200-299"  
    port                = "traffic-port"
    protocol            = "HTTP"  
    healthy_threshold   = 2
    unhealthy_threshold = 2
    timeout             = 60
    interval            = 120
  }
}

resource "aws_alb_listener" "http" {
  load_balancer_arn = aws_alb.lb.arn
  port = 80
  protocol = "HTTP"

  default_action {
    type = "redirect"

    redirect {
      port = "443"
      protocol = "HTTPS"
      status_code = "HTTP_301"
    }
  }
}

resource "aws_alb_listener" "https" {
  load_balancer_arn = aws_alb.lb.arn
  port = 443
  protocol = "HTTPS"
  ssl_policy = "ELBSecurityPolicy-2016-08"
  certificate_arn = var.acm_certificate.arn

  default_action {
    type             = "forward"
    target_group_arn = aws_alb_target_group.target_group.arn
  }
}