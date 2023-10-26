pipeline {
    agent any
    stages {
        stage('Git Checkout'){
            steps{
                 git 'https://github.com/rewqla/DevOps_demo_3.git'
            }
        }
        stage('Terraform init'){
            steps{
                dir('Terraform') {
                    sh 'terraform init'
                }
            }
        }
        stage('Create Ecr and Rds'){
            steps{
             dir('Terraform') {
                    withCredentials([[
                        $class: 'AmazonWebServicesCredentialsBinding',
                        credentialsId: "terraform-credentials",
                        accessKeyVariable: 'AWS_ACCESS_KEY_ID',
                        secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                            sh 'terraform apply --target=module.vpc --target=module.subnets  --target=module.security_groups  --target=module.rds --target=module.ecr --auto-approve'
                    }
                }
            }
        }
        stage('Build docker and push to ecr')
        {
            agent {
                label 'docker-slave' 
            }
            steps{
                git 'https://github.com/rewqla/DevOps_demo_3.git'
                dir('OilShop') {
                    withCredentials([[
                        $class: 'AmazonWebServicesCredentialsBinding',
                        credentialsId: "terraform-credentials",
                        accessKeyVariable: 'AWS_ACCESS_KEY_ID',
                        secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                            sh 'aws ecr get-login-password --region eu-north-1 | docker login --username AWS --password-stdin 325033910552.dkr.ecr.eu-north-1.amazonaws.com'
                            sh 'docker build -t oilshop-repository .'
                            sh 'docker tag oilshop-repository:latest 325033910552.dkr.ecr.eu-north-1.amazonaws.com/oilshop-repository:latest'
                            sh 'docker push 325033910552.dkr.ecr.eu-north-1.amazonaws.com/oilshop-repository:latest'
                    }
                }
                sh 'docker rmi oilshop-repository'
            }
        }
        stage('Build another modules from terraform'){   
            steps{
                retry(2){
                    dir('Terraform') {
                        withCredentials([[
                            $class: 'AmazonWebServicesCredentialsBinding',
                            credentialsId: "terraform-credentials",
                            accessKeyVariable: 'AWS_ACCESS_KEY_ID',
                            secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                sh 'terraform apply --auto-approve'
                        }
                    }
                }
            }
        }
        stage('Destroy terraform'){ 
             when {
                expression { params.destroyTerraform == "true" }
            }
            steps {
                dir('Terraform') {
                    withCredentials([[
                    $class: 'AmazonWebServicesCredentialsBinding',
                    credentialsId: "terraform-credentials",
                    accessKeyVariable: 'AWS_ACCESS_KEY_ID',
                    secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                          sh 'terraform destroy --auto-approve'
                    }
                }
            }
        }
    }
}