# 디버그 컨테이너를 사용자 지정하는 방법과 Visual Studio 이 Dockerfile을 사용하여 더 빠른 디버깅을 위해 이미지를 빌드하는 방법을 알아보려면 https://aka.ms/customizecontainer를 참조하세요.

# 이 스테이지는 VS에서 빠른 모드로 실행할 때 사용됩니다(디버그 구성의 기본값).
FROM ubuntu:22.04 AS base

# 필수 패키지 설치 및 .NET 런타임 설치
RUN apt-get update && apt-get install -y \
    wget \
    curl \
    ca-certificates \
    apt-transport-https \
    software-properties-common \
    supervisor \
    && wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-runtime-9.0 \
    && rm -rf /var/lib/apt/lists/*

# ttyd 설치
RUN wget -O /usr/local/bin/ttyd https://github.com/tsl0922/ttyd/releases/latest/download/ttyd.x86_64 \
    && chmod +x /usr/local/bin/ttyd

# VS Code CLI (code tunnel) 설치
RUN curl -Lk 'https://code.visualstudio.com/sha/download?build=stable&os=cli-alpine-x64' --output vscode_cli.tar.gz \
    && tar -xf vscode_cli.tar.gz -C /usr/local/bin \
    && chmod +x /usr/local/bin/code \
    && rm vscode_cli.tar.gz

# 애플리케이션 사용자 생성
RUN groupadd --gid 1000 app \
    && useradd --uid 1000 --gid app --shell /bin/bash --create-home app

# Supervisor 설정 파일 생성
RUN mkdir -p /etc/supervisor/conf.d

WORKDIR /app


# 이 스테이지는 서비스 프로젝트를 빌드하는 데 사용됩니다.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ModuCoda/ModuCoda.csproj", "ModuCoda/"]
RUN dotnet restore "./ModuCoda/ModuCoda.csproj"
COPY . .
WORKDIR "/src/ModuCoda"
RUN dotnet build "./ModuCoda.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 이 스테이지는 최종 스테이지에 복사할 서비스 프로젝트를 게시하는 데 사용됩니다.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ModuCoda.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 이 스테이지는 프로덕션에서 사용되거나 VS에서 일반 모드로 실행할 때 사용됩니다(디버그 구성을 사용하지 않는 경우 기본값).
FROM base AS final
WORKDIR /app

# 애플리케이션 파일 복사
COPY --from=publish /app/publish .

# Supervisor 설정 파일들 복사
COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# ModuCoda 애플리케이션 포트만 노출 (ttyd, code tunnel은 내부에서만 사용)
EXPOSE 5000

# 애플리케이션 소유권 변경
RUN chown -R app:app /app

# 사용자 전환
USER app

# Supervisor를 통해 모든 서비스 시작
CMD ["/usr/bin/supervisord", "-c", "/etc/supervisor/supervisord.conf"]