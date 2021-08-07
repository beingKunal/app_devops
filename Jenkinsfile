pipeline {
  agent any

  environment {
    scannerHome = tool name: 'sonar_scanner_dotnet'
    registry = 'kunalnagarro/devops'
    properties = null
    docker_port = null
    username = 'kunal'
  }
//   options {
//     timestamp()
//     timeout(time: 1, unit: 'HOURS')
//     skipDefaultCheckout()
//     buildDiscarder(logRotator(
//         numToKeepStr: '3',
//       daysToKeepStr: '10'))
// }
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
      stage('Start SonarQube Analysis') {
        steps {
          echo "Start sonarQube Analysis Step"
          withSonarQubeEnv('Test_Sonar') {
            bat "${scannerHome}\\SonarScanner.MSBuild.exe begin /k:sonar_Kunal /n:sonar_Kunal /v:1.0"

          }
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
      stage("Stop sonarQube Analysis") {
        steps {
          echo "Stop sonarQube analysis"
          withSonarQubeEnv('Test_Sonar') {
            bat "${scannerHome}\\SonarScanner.MSBuild.exe end"
          }
        }
      }
      stage('Docker Image') {
        steps {
          echo "Docker image step"
          bat 'dotnet publish -c Release'
          bat "docker build -t i_${username}_master:${BUIld_NUMBER} --no-cache -f DevOps/Dockerfile ."
        }
      }
      stage('Move Image to docker hub'){
        steps{
         echo "Move Image to Docker Hub"
          bat "docker tag i_${username}_master:${BUIld_NUMBER} ${registry}:${BUILd_NUMBER}"
          withDockerRegistry([credentialsId: 'DockerHub', url: ""]) {
            bat "docker push ${registry}:${BUILD_NUMBER}"
          }
      }
      }
}
}
