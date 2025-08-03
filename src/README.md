# ModuCoda - Interactive Learning Environments

[![License: AGPL v3](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](https://www.gnu.org/licenses/agpl-3.0)
[![License: Commercial](https://img.shields.io/badge/License-Commercial-green.svg)](LICENSE-COMMERCIAL)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Supported-blue.svg)](https://www.docker.com/)
[![Cross Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-lightgrey.svg)](https://github.com/rkttu/moducoda)

> Note: This project is still in early development. Significant changes to the code may occur frequently without notice.

**ModuCoda** is an open-source, cross-platform interactive learning environment that provides the frontend functionality similar to KataCoda and KillerCoda. It serves as a YARP-based reverse proxy that integrates with external services like ttyd (web terminal) and VS Code tunnel to create seamless interactive tutorials, coding exercises, and hands-on learning experiences across Windows, macOS, and Linux platforms.

![Screenshot](./Screenshot.jpg)

## ?? Features

- **YARP Reverse Proxy**: High-performance reverse proxy using Microsoft's YARP (Yet Another Reverse Proxy)
- **Service Integration**: Seamlessly integrates with ttyd and VS Code tunnel services
- **Cross-Platform Support**: Runs natively on Windows, macOS, and Linux
- **Interactive Terminal**: Web-based terminal access via ttyd integration
- **Code Editor Integration**: VS Code tunnel integration for full IDE experience
- **Docker Support**: Container-ready deployment with Docker
- **Responsive Design**: Modern, mobile-friendly web interface with resizable panels
- **Real-time Execution**: Execute commands and see results instantly through integrated services
- **Educational Templates**: Pre-built interactive learning content

## ??? Architecture

ModuCoda acts as a central hub that proxies requests to various backend services:

- **Core**: ASP.NET Core 9.0 with YARP reverse proxy
- **Terminal Service**: ttyd (external service) for web-based terminal access
- **Code Editor**: VS Code tunnel (external service) for development environment
- **Frontend**: Modern HTML5/CSS3/JavaScript with responsive layout
- **Health Checks**: Built-in health monitoring for all integrated services
- **Containerization**: Docker support for easy deployment

### Service Architecture

```
忙式式式式式式式式式式式式式式式式式忖    忙式式式式式式式式式式式式式式式式式忖    忙式式式式式式式式式式式式式式式式式忖
弛                 弛    弛                 弛    弛                 弛
弛   Web Browser   弛?式式?弛    ModuCoda     弛?式式?弛  Backend Services弛
弛                 弛    弛  (YARP Proxy)   弛    弛                 弛
戌式式式式式式式式式式式式式式式式式戎    戌式式式式式式式式式式式式式式式式式戎    戌式式式式式式式式式式式式式式式式式戎
                                弛                       弛
                                ∪                       ∪
                       忙式式式式式式式式式式式式式式式式式忖    忙式式式式式式式式式式式式式式式式式忖
                       弛  Instructions   弛    弛      ttyd       弛
                       弛     Page        弛    弛   (Terminal)    弛
                       戌式式式式式式式式式式式式式式式式式戎    戌式式式式式式式式式式式式式式式式式戎
                                                       弛
                                                       ∪
                                              忙式式式式式式式式式式式式式式式式式忖
                                              弛   VS Code       弛
                                              弛   (Tunnel)      弛
                                              戌式式式式式式式式式式式式式式式式式戎
```

## ?? Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [ttyd](https://github.com/tsl0922/ttyd) (for terminal functionality)
- [Visual Studio Code](https://code.visualstudio.com/) with tunnel support
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

## ?? Installation & Setup

### Option 1: Local Development

1. **Clone the repository**

   ```bash
   git clone https://github.com/rkttu/moducoda.git
   cd moducoda
   ```

2. **Setup Backend Services**
   
   The project includes a convenient script to launch required backend services:

   ```bash
   # On Windows
   cd src/ModuCoda
   .\launch_backend.cmd
   ```

   This script will automatically start:
   - **ttyd** on `http://localhost:7681` (web terminal)
   - **VS Code tunnel** on `http://localhost:8000` (code editor)

3. **Restore dependencies**

   ```bash
   cd src/ModuCoda
   dotnet restore
   ```

4. **Build the project**

   ```bash
   dotnet build
   ```

5. **Run the application**

   ```bash
   dotnet run
   ```

6. **Access the application**
   - Open your browser and navigate to `http://localhost:5000`
   - The interface provides tabbed access to both terminal and code editor
   - Interactive learning content is available in the left panel

### Option 2: Docker Deployment

1. **Clone the repository**

   ```bash
   git clone https://github.com/rkttu/moducoda.git
   cd moducoda
   ```

2. **Build Docker image**

   ```bash
   docker build -t moducoda -f src/ModuCoda/Dockerfile .
   ```

3. **Run container**

   ```bash
   docker run -p 5000:8080 moducoda
   ```

4. **Access the application**
   - Open your browser and navigate to `http://localhost:5000`

## ?? Usage

### Quick Start

1. **Start Backend Services**: Run `launch_backend.cmd` to start ttyd and VS Code tunnel
2. **Launch ModuCoda**: Run `dotnet run` in the ModuCoda directory
3. **Open Browser**: Navigate to `http://localhost:5000`
4. **Start Learning**: Follow the interactive instructions in the left panel

### Interface Overview

- **Left Panel**: Interactive learning instructions and tutorials
- **Right Panel**: Tabbed interface with:
  - **Terminal Tab**: Web-based terminal via ttyd
  - **Code Editor Tab**: Full VS Code experience via tunnel
- **Resizable Layout**: Drag the divider to adjust panel sizes
- **Responsive Design**: Adapts to different screen sizes

### Supported Learning Scenarios

- **Linux/Unix Command Line**: Basic to advanced shell commands
- **Programming Languages**: Python, JavaScript, C#, and more
- **DevOps Tools**: Docker, Kubernetes, CI/CD pipelines
- **System Administration**: File management, process control, networking
- **Development Workflows**: Git, package managers, build tools

## ??? Configuration

The application can be configured through:

- **appsettings.json**: Basic application settings
- **Environment Variables**: Runtime configuration
- **Docker Environment**: Container-specific settings

### Key Configuration Options

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*",
  "ttydAddress": "http://localhost:7681/",
  "codeAddress": "http://localhost:8000/"
}
```

### Service Configuration

- **ttyd**: Configure terminal settings and access in `launch_backend.cmd`
- **VS Code**: Configure tunnel settings and extensions as needed
- **YARP Proxy**: Route configuration in `Program.cs`

## ?? Service Integration

ModuCoda uses YARP reverse proxy to integrate with various services:

- **Terminal Service**: `/ttyd/` - Proxies to ttyd web terminal
- **Code Editor**: `/vscode/` - Proxies to VS Code tunnel
- **Instructions**: `/instructions/` - Serves interactive learning content
- **Health Checks**: `/healthz` - Monitors service health

### Proxy Routes

| Route | Target Service | Purpose |
|-------|---------------|---------|
| `/ttyd/*` | ttyd (port 7681) | Web-based terminal access |
| `/vscode/*` | VS Code tunnel (port 8000) | Code editor and IDE features |
| `/instructions` | Built-in service | Interactive learning content |
| `/healthz` | Built-in service | Health monitoring |

## ?? Contributing

We welcome contributions from the community! Here's how you can help:

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/amazing-feature`
3. **Make your changes** and add tests if applicable
4. **Commit your changes**: `git commit -m 'Add amazing feature'`
5. **Push to the branch**: `git push origin feature/amazing-feature`
6. **Open a Pull Request**

### Development Guidelines

- Follow C# coding conventions
- Add unit tests for new features
- Update documentation as needed
- Ensure cross-platform compatibility
- Test proxy configurations thoroughly

## ?? Roadmap

- [ ] **Plugin System**: Extensible architecture for custom learning modules
- [ ] **User Management**: Authentication and progress tracking
- [ ] **Content Management**: CMS for creating and managing tutorials
- [ ] **Multi-language Support**: Internationalization (i18n)
- [ ] **Analytics Dashboard**: Learning progress and usage statistics
- [ ] **API Integration**: RESTful APIs for external system integration
- [ ] **Collaborative Features**: Multi-user sessions and sharing
- [ ] **Service Discovery**: Automatic detection of available backend services
- [ ] **Load Balancing**: Multiple backend service instances support

## ?? License

ModuCoda is available under a **dual licensing model**:

### 1. Open Source License (AGPL-3.0)

This project is licensed under the GNU Affero General Public License v3.0 (AGPL-3.0) for open source use - see the [LICENSE](LICENSE) file for details.

**AGPL-3.0 License Summary:**

- **Source Code Availability**: If you modify and deploy this software on a network server, you must provide the source code to users
- **Same License**: Derivative works must be licensed under AGPL-3.0
- **Network Use**: Unlike GPL, AGPL covers network use as distribution
- **Commercial Use**: Allowed, but with the above requirements

### 2. Commercial License

For commercial deployments that require proprietary modifications or cannot comply with AGPL-3.0 requirements, a commercial license is available - see the [LICENSE-COMMERCIAL](LICENSE-COMMERCIAL) file for details.

**Commercial License Benefits:**

- **No Source Code Disclosure**: Deploy and modify without source code sharing requirements
- **Proprietary Integration**: Integrate into commercial products and SaaS offerings
- **Flexible Usage**: Use in closed-source environments
- **Commercial Support**: Professional support options available

### Which License Should You Choose?

| Use Case | Recommended License |
|----------|-------------------|
| Open source projects | AGPL-3.0 |
| Educational/Research use | AGPL-3.0 |
| Internal enterprise use with source sharing | AGPL-3.0 |
| Commercial SaaS without source disclosure | Commercial License |
| Integration into proprietary software | Commercial License |
| Redistribution in commercial products | Commercial License |

### Getting a Commercial License

For commercial licensing inquiries, please contact the project maintainers:

- **GitHub Issues**: [Create a licensing inquiry](https://github.com/rkttu/moducoda/issues/new?template=commercial-license.md)
- **Email**: [Contact information to be provided]
- **Website**: [Commercial licensing page to be added]

We offer flexible commercial licensing terms based on your specific use case and requirements.

## ?? Acknowledgments

- **KataCoda & KillerCoda**: Inspiration for interactive learning environments
- **Microsoft YARP**: High-performance reverse proxy solution
- **ttyd**: Excellent web-based terminal implementation
- **VS Code**: Powerful development environment with tunnel support
- **ASP.NET Core**: Robust web framework foundation

## ?? Support

- **Issues**: [GitHub Issues](https://github.com/rkttu/moducoda/issues)
- **Discussions**: [GitHub Discussions](https://github.com/rkttu/moducoda/discussions)
- **Documentation**: [Wiki](https://github.com/rkttu/moducoda/wiki)

## ?? Show Your Support

If you find ModuCoda helpful, please consider:

- ? **Starring the repository**
- ?? **Reporting bugs and issues**
- ?? **Suggesting new features**
- ?? **Contributing code or documentation**
- ?? **Sharing with others who might benefit**

---

**ModuCoda** - Making interactive learning accessible to everyone, everywhere. ??