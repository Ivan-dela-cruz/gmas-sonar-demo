name: "sonar_cloud_scan_github_actions"

on:
  workflow_dispatch:

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      # 1. Clonar el repositorio
      - uses: actions/checkout@v3
        with:
          fetch-depth: 0
          
      # 2. Configurar .NET
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: 7.0  # Cambia según la versión de .NET que uses

      # 3. Restaurar dependencias
      - name: Restore dependencies
        run: dotnet restore src/BUE.inscriptions.Services.sln

      # 4. Construir el proyecto
      - name: Build the project
        run: dotnet build src/BUE.inscriptions.Services.sln --no-restore

      # 5. Ejecutar pruebas y generar cobertura
      - name: Run tests and generate coverage
        run: |
          dotnet test src/BUE.inscriptions.Services.sln --no-build --collect:"XPlat Code Coverage" \
            --results-directory TestResults

      # 6. Instalar ReportGenerator
      - name: Install ReportGenerator
        run: dotnet tool install --global dotnet-reportgenerator-globaltool

      # 7. Agregar herramientas de .NET al PATH
      - name: Add .NET tools to PATH
        run: echo "$HOME/.dotnet/tools" >> $GITHUB_PATH

      # 8. Convertir cobertura al formato requerido por SonarCloud
      - name: Convert coverage report to XML
        run: |
          reportgenerator \
            -reports:TestResults/**/coverage.cobertura.xml \
            -targetdir:TestResults/CoverageReport \
            -reporttypes:SonarQube

      # 9. Escaneo con SonarCloud
      - name: SonarCloud Scan
        uses: sonarsource/sonarcloud-github-action@master
        env:
          GITHUB_TOKEN: ${{ secrets.GIT_TOKEN }}
          SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
        with:
          args: >
            -Dsonar.organization=ivan-dela-cruz
            -Dsonar.projectKey=gmas-sonar-demo
            -Dsonar.cs.opencover.reportsPaths=TestResults/CoverageReport/SonarQube.xml
