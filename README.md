# ModuCoda - Interactive Learning Environments

[![License: AGPL v3](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](https://www.gnu.org/licenses/agpl-3.0)
[![License: Commercial](https://img.shields.io/badge/License-Commercial-green.svg)](LICENSE-COMMERCIAL)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![Docker](https://img.shields.io/badge/Docker-Supported-blue.svg)](https://www.docker.com/)
[![Cross Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-lightgrey.svg)](https://github.com/rkttu/moducoda)

> Note: This project is still in early development. Significant changes to the code may occur frequently without notice.

**ModuCoda** is an open-source, cross-platform interactive learning environment that provides the frontend functionality similar to KataCoda and KillerCoda. It enables developers, educators, and learners to create and run interactive tutorials, coding exercises, and hands-on learning experiences across Windows, macOS, and Linux platforms.

![Screenshot](./Screenshot.jpg)

## 🚀 Features

- **Cross-Platform Support**: Runs natively on Windows, macOS, and Linux
- **Interactive Terminal**: Integrated web-based terminal using ttyd
- **Code Editor Integration**: Built-in code editor with syntax highlighting
- **Docker Support**: Container-ready deployment with Docker
- **Reverse Proxy**: YARP-based reverse proxy for seamless service integration
- **Responsive Design**: Modern, mobile-friendly web interface
- **Real-time Execution**: Execute commands and see results instantly
- **Educational Templates**: Pre-built templates for common learning scenarios

## 🏗️ Architecture

ModuCoda is built with:

- **Backend**: ASP.NET Core 9.0 with C#
- **Reverse Proxy**: YARP (Yet Another Reverse Proxy)
- **Terminal**: ttyd for web-based terminal access
- **Frontend**: Modern HTML5/CSS3/JavaScript
- **Containerization**: Docker support for easy deployment

## 🔧 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Docker](https://www.docker.com/) (optional, for containerized deployment)
- [ttyd](https://github.com/tsl0922/ttyd) (for terminal functionality)

## 📦 Installation

### Option 1: Local Development

1. **Clone the repository**

   ```bash
   git clone https://github.com/rkttu/moducoda.git
   cd moducoda
   ```

2. **Restore dependencies**

   ```bash
   cd src/ModuCoda
   dotnet restore
   ```

3. **Build the project**

   ```bash
   dotnet build
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

5. **Access the application**
   - Open your browser and navigate to `http://localhost:5000`

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

## 🎯 Usage

### Creating Learning Content

1. **Navigate to the web interface** at `http://localhost:5000`
2. **Use the integrated terminal** for command-line interactions
3. **Access the code editor** for file editing and code examples
4. **Follow interactive tutorials** with step-by-step guidance

### Supported Learning Scenarios

- **Linux/Unix Command Line**: Basic to advanced shell commands
- **Programming Languages**: Python, JavaScript, C#, and more
- **DevOps Tools**: Docker, Kubernetes, CI/CD pipelines
- **System Administration**: File management, process control, networking
- **Development Workflows**: Git, package managers, build tools

## 🛠️ Configuration

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
  "AllowedHosts": "*"
}
```

## 🔌 Service Integration

ModuCoda uses YARP reverse proxy to integrate with various services:

- **Terminal Service**: `/ttyd/` - Web-based terminal access
- **Code Editor**: `/code/` - Integrated development environment
- **Static Content**: Serves learning materials and resources

## 🤝 Contributing

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

## 📋 Roadmap

- [ ] **Plugin System**: Extensible architecture for custom learning modules
- [ ] **User Management**: Authentication and progress tracking
- [ ] **Content Management**: CMS for creating and managing tutorials
- [ ] **Multi-language Support**: Internationalization (i18n)
- [ ] **Analytics Dashboard**: Learning progress and usage statistics
- [ ] **API Integration**: RESTful APIs for external system integration
- [ ] **Collaborative Features**: Multi-user sessions and sharing

## 📄 License

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

## 🙏 Acknowledgments

- **KataCoda & KillerCoda**: Inspiration for interactive learning environments
- **ttyd**: Web-based terminal implementation
- **YARP**: Microsoft's reverse proxy solution
- **ASP.NET Core**: Robust web framework foundation

## 📞 Support

- **Issues**: [GitHub Issues](https://github.com/rkttu/moducoda/issues)
- **Discussions**: [GitHub Discussions](https://github.com/rkttu/moducoda/discussions)
- **Documentation**: [Wiki](https://github.com/rkttu/moducoda/wiki)

## 🌟 Show Your Support

If you find ModuCoda helpful, please consider:

- ⭐ **Starring the repository**
- 🐛 **Reporting bugs and issues**
- 💡 **Suggesting new features**
- 🤝 **Contributing code or documentation**
- 📢 **Sharing with others who might benefit**

---

**ModuCoda** - Making interactive learning accessible to everyone, everywhere. 🚀
