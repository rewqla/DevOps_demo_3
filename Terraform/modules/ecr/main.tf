resource "aws_ecr_repository" "ecr" {   
    name = var.repository_name
    force_delete = true

    image_scanning_configuration {
        scan_on_push = true
    }
}

resource "aws_ecr_lifecycle_policy" "lanandra_ip_reader" {
  repository = aws_ecr_repository.ecr.name

  policy = jsonencode(
  {
    "rules": [
        {
            "rulePriority": 1,
            "description": "Expire images older than 7 days",
            "selection": {
                "tagStatus": "untagged",
                "countType": "sinceImagePushed",
                "countUnit": "days",
                "countNumber": 7
            },
            "action": {
                "type": "expire"
            }
        }
    ]
  })
}