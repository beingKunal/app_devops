pipeline {
  agent any

  environment {
    scannerHome = tool name: 'sonar_scanner_dotnet'
    registry = 'kunalnagarro/devops'
    properties = null
    docker_port = null
    username = 'kunal'
  }
  options {
    timestamps()
    timeout(time: 1, unit: 'HOURS')
    skipDefaultCheckout()
    buildDiscarder(logRotator(
        numToKeepStr: '3',
      daysToKeepStr: '10'))
}
stages {
  stage('Start') {
    steps {
      echo "Checking out git repo"
      checkout scm
//           script {
//             docker_port: 7200
//             properties = readProperties file: 'user.properties'
//           }
        }
      }
      stage('nuget restore') {
        steps {
//           echo "Running Build ${JOB_NAME} # ${BUILD_NUMBER} for ${properties['user.employeeid']} with docker as ${docker_port}"
          echo "Nuget Restore step"
          bat "dotnet restore"
        }
      }
      stage('Code Build') {
        steps {
          echo "Clean Previous Build"
          bat "dotnet clean"

          // Build the project and all its dependencies
          echo "Code Build"
          bat 'dotnet build -c Release -o "DevOps_Assignment/app/build"'
        }
      }
      stage('Docker Image') {
        steps {
          echo "Docker image step"
          bat 'dotnet publish -c Release'
          bat "docker build -t i_${username}_develop:${BUIld_NUMBER} --no-cache -f Dockerfile ."
        }
      }
      stage('Move Image to docker hub'){
        steps{
         echo "Move Image to Docker Hub"
          bat "docker tag i_${username}_develop:${BUIld_NUMBER} ${registry}:${BUILd_NUMBER}"
          withDockerRegistry([credentialsId: 'DockerHub', url: "https://hub.docker.com/repository/docker/kunalnagarro/devops"]) {
            bat "docker push ${registry}:${BUILD_NUMBER}"
          }
      }
      }
        stage("Docker Deploymnet") {
        steps {
		
          echo "Docker Deployment"
          bat "docker run --name c_${username}_develop -d -p 7300:80 ${registry}:${BUILD_NUMBER}"
        }
      }
}
}
