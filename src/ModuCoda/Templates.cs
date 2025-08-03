internal static class Templates
{
    internal static string InstructionPage() => $$"""
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Linux 기초 명령어 학습</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: #333;
            line-height: 1.6;
            padding: 0;
            margin: 0;
            min-height: 100vh;
        }

        .container {
            max-width: 100%;
            margin: 0;
            padding: 20px;
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(10px);
            min-height: 100vh;
        }

        .header {
            text-align: center;
            margin-bottom: 30px;
            padding-bottom: 20px;
            border-bottom: 3px solid #4f46e5;
        }

        .header h1 {
            color: #4f46e5;
            font-size: 28px;
            font-weight: 700;
            margin-bottom: 10px;
            text-shadow: 0 2px 4px rgba(79, 70, 229, 0.1);
        }

        .header p {
            color: #6b7280;
            font-size: 16px;
            font-weight: 400;
        }

        .progress-bar {
            background: #e5e7eb;
            border-radius: 20px;
            height: 8px;
            margin: 20px 0;
            overflow: hidden;
        }

        .progress-fill {
            background: linear-gradient(90deg, #4f46e5, #7c3aed);
            height: 100%;
            width: 25%;
            border-radius: 20px;
            transition: width 0.3s ease;
        }

        .step {
            background: white;
            border-radius: 12px;
            padding: 24px;
            margin-bottom: 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            border-left: 4px solid #4f46e5;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

        .step:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.15);
        }

        .step-number {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 32px;
            height: 32px;
            background: linear-gradient(135deg, #4f46e5, #7c3aed);
            color: white;
            border-radius: 50%;
            font-weight: 700;
            font-size: 16px;
            margin-right: 12px;
            margin-bottom: 16px;
        }

        .step h3 {
            display: inline-block;
            color: #1f2937;
            font-size: 20px;
            font-weight: 600;
            margin-bottom: 16px;
            vertical-align: top;
            line-height: 32px;
        }

        .step p {
            color: #4b5563;
            margin-bottom: 16px;
            font-size: 15px;
        }

        .command-box {
            background: #1f2937;
            color: #f9fafb;
            padding: 16px;
            border-radius: 8px;
            font-family: 'Consolas', 'Monaco', monospace;
            font-size: 14px;
            margin: 16px 0;
            position: relative;
            border-left: 4px solid #10b981;
            overflow-x: auto;
        }

        .command-box::before {
            content: '$';
            color: #10b981;
            margin-right: 8px;
            font-weight: bold;
        }

        .command-actions {
            position: absolute;
            top: 8px;
            right: 8px;
            display: flex;
            gap: 6px;
        }

        .copy-btn, .exec-btn {
            border: none;
            border-radius: 6px;
            padding: 6px 12px;
            font-size: 12px;
            cursor: pointer;
            transition: all 0.2s ease;
            font-weight: 500;
        }

        .copy-btn {
            background: #6b7280;
            color: white;
        }

        .copy-btn:hover {
            background: #4b5563;
        }

        .exec-btn {
            background: #10b981;
            color: white;
        }

        .exec-btn:hover {
            background: #059669;
            transform: translateY(-1px);
            box-shadow: 0 4px 8px rgba(16, 185, 129, 0.3);
        }

        .copy-btn:active, .exec-btn:active {
            transform: scale(0.95);
        }

        .expected-output {
            background: #f3f4f6;
            border: 1px solid #d1d5db;
            border-radius: 8px;
            padding: 16px;
            margin: 16px 0;
            font-family: 'Consolas', 'Monaco', monospace;
            font-size: 14px;
            color: #374151;
        }

        .expected-output::before {
            content: '💡 예상 출력:';
            display: block;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            font-weight: 600;
            color: #059669;
            margin-bottom: 8px;
            font-size: 13px;
        }

        .tip {
            background: linear-gradient(135deg, #fef3c7, #fde68a);
            border: 1px solid #f59e0b;
            border-radius: 8px;
            padding: 16px;
            margin: 20px 0;
        }

        .tip::before {
            content: '💡';
            font-size: 18px;
            margin-right: 8px;
        }

        .tip strong {
            color: #92400e;
        }

        .warning {
            background: linear-gradient(135deg, #fee2e2, #fecaca);
            border: 1px solid #ef4444;
            border-radius: 8px;
            padding: 16px;
            margin: 20px 0;
        }

        .warning::before {
            content: '⚠️';
            font-size: 18px;
            margin-right: 8px;
        }

        .warning strong {
            color: #dc2626;
        }

        .next-btn {
            background: linear-gradient(135deg, #4f46e5, #7c3aed);
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s ease;
            display: block;
            margin: 30px auto 0;
            min-width: 140px;
        }

        .next-btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 20px rgba(79, 70, 229, 0.3);
        }

        .next-btn:active {
            transform: translateY(0);
        }

        .badge {
            display: inline-block;
            background: #10b981;
            color: white;
            padding: 4px 8px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 600;
            margin-left: 8px;
        }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .step {
            animation: fadeInUp 0.6s ease forwards;
        }

        .step:nth-child(2) { animation-delay: 0.1s; }
        .step:nth-child(3) { animation-delay: 0.2s; }
        .step:nth-child(4) { animation-delay: 0.3s; }

        @media (max-width: 768px) {
            .container {
                padding: 15px;
            }
            
            .header h1 {
                font-size: 24px;
            }
            
            .step {
                padding: 20px;
            }
            
            .command-box {
                font-size: 13px;
                padding: 14px;
            }
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <h1>🐧 Linux 기초 명령어 학습</h1>
            <p>터미널을 활용한 실습형 학습</p>
            <div class="progress-bar">
                <div class="progress-fill"></div>
            </div>
            <small>진행률: 1/4 단계 완료</small>
        </div>

        <div class="step">
            <span class="step-number">1</span>
            <h3>현재 위치 확인하기 <span class="badge">기초</span></h3>
            <p>Linux에서 가장 기본적인 명령어인 <strong>pwd</strong>를 사용하여 현재 작업 디렉토리를 확인해보겠습니다.</p>
            
            <div class="command-box">
                <div class="command-actions">
                    <button class="copy-btn" onclick="copyCommand('pwd')">복사</button>
                    <button class="exec-btn" onclick="launchCommand('pwd')">실행</button>
                </div>
                pwd
            </div>
            
            <div class="expected-output">
/home/user
            </div>
            
            <p><code>pwd</code>는 "Print Working Directory"의 줄임말로, 현재 있는 폴더의 전체 경로를 보여줍니다.</p>
        </div>

        <div class="step">
            <span class="step-number">2</span>
            <h3>디렉토리 내용 확인하기 <span class="badge">기초</span></h3>
            <p><strong>ls</strong> 명령어로 현재 디렉토리에 있는 파일과 폴더들을 확인해보세요.</p>
            
            <div class="command-box">
                <div class="command-actions">
                    <button class="copy-btn" onclick="copyCommand('ls -la')">복사</button>
                    <button class="exec-btn" onclick="launchCommand('ls -la')">실행</button>
                </div>
                ls -la
            </div>
            
            <div class="expected-output">
total 24
drwxr-xr-x 3 user user 4096 Jan 15 10:30 .
drwxr-xr-x 3 root root 4096 Jan 15 10:25 ..
-rw-r--r-- 1 user user  220 Jan 15 10:25 .bash_logout
-rw-r--r-- 1 user user 3526 Jan 15 10:25 .bashrc
drwxr-xr-x 2 user user 4096 Jan 15 10:30 Documents
            </div>
            
            <div class="tip">
                <strong>팁:</strong> <code>-la</code> 옵션은 숨겨진 파일(.)까지 자세한 정보와 함께 보여줍니다. <code>-l</code>은 자세한 정보, <code>-a</code>는 모든 파일을 의미합니다.
            </div>
        </div>

        <div class="step">
            <span class="step-number">3</span>
            <h3>새 디렉토리 만들기 <span class="badge">실습</span></h3>
            <p><strong>mkdir</strong> 명령어로 새로운 폴더를 만들어보겠습니다.</p>
            
            <div class="command-box">
                <div class="command-actions">
                    <button class="copy-btn" onclick="copyCommand('mkdir moducoda-practice')">복사</button>
                    <button class="exec-btn" onclick="launchCommand('mkdir moducoda-practice')">실행</button>
                </div>
                mkdir moducoda-practice
            </div>
            
            <p>이제 <code>ls</code> 명령어로 새로 만든 디렉토리가 생성되었는지 확인해보세요:</p>
            
            <div class="command-box">
                <div class="command-actions">
                    <button class="copy-btn" onclick="copyCommand('ls')">복사</button>
                    <button class="exec-btn" onclick="launchCommand('ls')">실행</button>
                </div>
                ls
            </div>
            
            <div class="expected-output">
Documents  moducoda-practice
            </div>
        </div>

        <div class="step">
            <span class="step-number">4</span>
            <h3>디렉토리 이동하기 <span class="badge">실습</span></h3>
            <p><strong>cd</strong> 명령어로 방금 만든 디렉토리로 이동해보겠습니다.</p>
            
            <div class="command-box">
                <button class="copy-btn" onclick="copyCommand('cd moducoda-practice')">복사</button>
                cd moducoda-practice
            </div>
            
            <p>이동했는지 확인하기 위해 다시 <code>pwd</code>를 실행해보세요:</p>
            
            <div class="command-box">
                <button class="copy-btn" onclick="copyCommand('pwd')">복사</button>
                pwd
            </div>
            
            <div class="expected-output">
/home/user/moducoda-practice
            </div>
            
            <div class="warning">
                <strong>주의:</strong> Linux는 대소문자를 구분합니다. 디렉토리나 파일 이름을 정확히 입력해야 합니다.
            </div>
        </div>

        <div class="tip">
            <strong>다음 단계 미리보기:</strong> 다음에는 파일 생성, 편집, 그리고 권한 관리에 대해 학습하게 됩니다. 현재 학습한 기본 명령어들을 충분히 연습해보세요!
        </div>

        <button class="next-btn" onclick="nextStep()">다음 단계로 →</button>
    </div>

    <script>
        // 터미널 명령어 실행 함수
        function launchCommand(command) {
            try {
                // 부모 창에서 터미널 iframe 찾기 (iframe 중첩 구조 고려)
                var terminalFrame = window.parent.document.querySelector('iframe.terminal-iframe');
                
                // 터미널에 명령어 붙여넣기
                terminalFrame.contentWindow.term.paste(command);
                
                // Enter 키 이벤트 발생
                var inputArea = terminalFrame.contentDocument.querySelector("textarea.xterm-helper-textarea");
                inputArea.dispatchEvent(new KeyboardEvent('keypress', {charCode: 13}));
                
                // 성공 피드백
                event.target.textContent = '실행됨!';
                event.target.style.background = '#059669';
                
                setTimeout(() => {
                    event.target.textContent = '실행';
                    event.target.style.background = '#10b981';
                }, 2000);                
            } catch (error) {
                console.error('명령어 실행 실패:', error);
                
                // 실패 시 복사로 폴백
                copyCommand(command);
                
                // 사용자에게 안내
                event.target.textContent = '복사됨';
                event.target.style.background = '#f59e0b';
                
                setTimeout(() => {
                    event.target.textContent = '실행';
                    event.target.style.background = '#10b981';
                }, 2000);
                
                // 선택적: 사용자에게 알림
                console.log('터미널 직접 실행에 실패하여 클립보드에 복사했습니다. 터미널에서 Ctrl+V로 붙여넣으세요.');
            }
        }

        function copyCommand(command) {
            navigator.clipboard.writeText(command).then(function() {
                // 복사 성공 피드백
                event.target.textContent = '복사됨!';
                event.target.style.background = '#10b981';
                
                setTimeout(() => {
                    event.target.textContent = '복사';
                    event.target.style.background = '#6b7280';
                }, 2000);
            }).catch(function(err) {
                console.error('복사 실패:', err);
                // 폴백: 텍스트 선택
                const textArea = document.createElement('textarea');
                textArea.value = command;
                document.body.appendChild(textArea);
                textArea.select();
                document.execCommand('copy');
                document.body.removeChild(textArea);
                
                event.target.textContent = '복사됨!';
                setTimeout(() => {
                    event.target.textContent = '복사';
                }, 2000);
            });
        }

        function nextStep() {
            // 다음 스텝으로 이동하는 로직 (실제 구현에서는 라우팅 등으로 처리)
            alert('다음 단계로 이동합니다!\n\n실제 구현에서는 새로운 학습 콘텐츠가 로드됩니다.');
            
            // 진행률 업데이트 예시
            const progressFill = document.querySelector('.progress-fill');
            progressFill.style.width = '50%';
        }

        // 페이지 로드 시 애니메이션 효과
        document.addEventListener('DOMContentLoaded', function() {
            const steps = document.querySelectorAll('.step');
            steps.forEach((step, index) => {
                step.style.opacity = '0';
                step.style.transform = 'translateY(20px)';
                
                setTimeout(() => {
                    step.style.transition = 'all 0.6s ease';
                    step.style.opacity = '1';
                    step.style.transform = 'translateY(0)';
                }, index * 150);
            });
        });

        // 키보드 단축키 지원
        document.addEventListener('keydown', function(e) {
            if (e.ctrlKey && e.key === 'Enter') {
                nextStep();
            }
        });
    </script>
</body>
</html>
""";

    internal static string RenderLayoutPage() => $$"""
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>ModuCoda Interactive Learning Environments</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #1a1a1a;
            color: #e0e0e0;
            height: 100vh;
            overflow: hidden;
        }

        .container {
            display: flex;
            height: 100vh;
            position: relative;
        }

        .sidebar {
            width: 30%;
            min-width: 200px;
            background: linear-gradient(135deg, #2c3e50 0%, #34495e 100%);
            border-right: 3px solid #3498db;
            display: flex;
            flex-direction: column;
            overflow: hidden;
            resize: horizontal;
        }

        .resizer {
            width: 6px;
            background: #3498db;
            cursor: col-resize;
            position: relative;
            z-index: 10;
            transition: background-color 0.2s;
        }

        .resizer:hover {
            background: #2980b9;
        }

        .resizer::after {
            content: '';
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 2px;
            height: 40px;
            background: rgba(255, 255, 255, 0.3);
            border-radius: 2px;
        }

        .header {
            background-color: #2980b9;
            padding: 20px;
            border-bottom: 2px solid #3498db;
        }

        .header h1 {
            color: white;
            font-size: 24px;
            font-weight: 600;
            margin-bottom: 8px;
        }

        .header p {
            color: #ecf0f1;
            font-size: 14px;
            opacity: 0.9;
        }

        .instruction-area {
            flex: 1;
            background-color: #000;
        }

        .instruction-iframe {
            width: 100%;
            height: 100%;
            border: none;
            background-color: #fff;
        }

        .main-area {
            flex: 1;
            background-color: #000;
            position: relative;
            min-width: 300px;
            display: flex;
            flex-direction: column;
        }

        .tab-header {
            background: linear-gradient(135deg, #2c3e50 0%, #34495e 100%);
            border-bottom: 2px solid #3498db;
            display: flex;
            align-items: center;
            height: 60px;
        }

        .tab-navigation {
            display: flex;
            flex: 1;
        }

        .tab-button {
            background: none;
            border: none;
            color: #ecf0f1;
            font-size: 16px;
            font-weight: 500;
            padding: 16px 24px;
            cursor: pointer;
            transition: all 0.2s ease;
            border-bottom: 3px solid transparent;
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .tab-button:hover {
            background-color: rgba(255, 255, 255, 0.1);
            color: #3498db;
        }

        .tab-button.active {
            background-color: rgba(52, 152, 219, 0.2);
            color: #3498db;
            border-bottom-color: #3498db;
        }

        .terminal-status {
            display: flex;
            align-items: center;
            gap: 8px;
            padding: 0 20px;
        }

        .status-dot {
            width: 10px;
            height: 10px;
            border-radius: 50%;
            background-color: #2ecc71;
            animation: pulse 2s infinite;
        }

        @keyframes pulse {
            0% { opacity: 1; }
            50% { opacity: 0.5; }
            100% { opacity: 1; }
        }

        .status-text {
            color: #2ecc71;
            font-size: 12px;
            font-weight: 500;
        }

        .tab-content {
            flex: 1;
            position: relative;
            overflow: hidden;
        }

        .tab-pane {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            opacity: 0;
            visibility: hidden;
            transition: opacity 0.3s ease, visibility 0.3s ease;
        }

        .tab-pane.active {
            opacity: 1;
            visibility: visible;
        }

        .tab-iframe {
            width: 100%;
            height: 100%;
            border: none;
            background-color: #000;
        }

        @media (max-width: 768px) {
            .container {
                flex-direction: column;
            }
            
            .sidebar {
                width: 100% !important;
                height: 40%;
                min-height: 200px;
                resize: vertical;
            }
            
            .resizer {
                width: 100%;
                height: 6px;
                cursor: row-resize;
            }

            .resizer::after {
                width: 40px;
                height: 2px;
            }
            
            .main-area {
                width: 100%;
                flex: 1;
                min-height: 300px;
            }

            .tab-button {
                font-size: 14px;
                padding: 12px 16px;
            }
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="sidebar">
            <div class="header">
                <h1>Moducoda</h1>
                <p>Modular Learning Platform for Everyone</p>
            </div>
            <div class="instruction-area">
                <iframe 
                    class="instruction-iframe" 
                    src="/instructions"
                    title="Instructions">
                </iframe>
            </div>
        </div>

        <div class="resizer" id="resizer"></div>

        <div class="main-area">
            <div class="tab-header">
                <div class="tab-navigation">
                    <button class="tab-button active" data-tab="terminal">
                        🖥️ Terminal
                    </button>
                    <button class="tab-button" data-tab="code">
                        📝 Code Editor
                    </button>
                </div>
                <div class="terminal-status">
                    <div class="status-dot"></div>
                    <span class="status-text">Connected</span>
                </div>
            </div>
            
            <div class="tab-content">
                <div class="tab-pane active" id="terminal-pane">
                    <iframe 
                        class="tab-iframe terminal-iframe" 
                        src="/ttyd/"
                        title="Interactive Terminal">
                    </iframe>
                </div>
                
                <div class="tab-pane" id="code-pane">
                    <iframe 
                        class="tab-iframe code-iframe" 
                        src="/vscode"
                        title="Code Editor">
                    </iframe>
                </div>
            </div>
        </div>
    </div>

    <script>
        // 탭 전환 기능
        class TabManager {
            constructor() {
                this.tabButtons = document.querySelectorAll('.tab-button');
                this.tabPanes = document.querySelectorAll('.tab-pane');
                this.init();
            }

            init() {
                this.tabButtons.forEach(button => {
                    button.addEventListener('click', (e) => {
                        const tabId = e.currentTarget.getAttribute('data-tab');
                        this.switchTab(tabId);
                    });
                });
            }

            switchTab(tabId) {
                // 모든 탭 버튼에서 active 클래스 제거
                this.tabButtons.forEach(button => {
                    button.classList.remove('active');
                });

                // 모든 탭 패널에서 active 클래스 제거
                this.tabPanes.forEach(pane => {
                    pane.classList.remove('active');
                });

                // 선택된 탭 버튼에 active 클래스 추가
                const activeButton = document.querySelector(`[data-tab="${tabId}"]`);
                if (activeButton) {
                    activeButton.classList.add('active');
                }

                // 선택된 탭 패널에 active 클래스 추가
                const activePane = document.getElementById(`${tabId}-pane`);
                if (activePane) {
                    activePane.classList.add('active');
                }
            }
        }

        // 동적 크기 조절 기능
        class ResizableLayout {
            constructor() {
                this.resizer = document.getElementById('resizer');
                this.sidebar = document.querySelector('.sidebar');
                this.container = document.querySelector('.container');
                this.isResizing = false;
                this.isMobile = window.innerWidth <= 768;
                
                this.init();
                this.handleResize();
            }

            init() {
                // resizer에서만 시작
                this.resizer.addEventListener('mousedown', this.startResize.bind(this));
                this.resizer.addEventListener('touchstart', this.startResize.bind(this), { passive: false });
                
                // document 전체에서 이벤트 처리
                document.addEventListener('mousemove', this.resize.bind(this));
                document.addEventListener('touchmove', this.resize.bind(this), { passive: false });
                document.addEventListener('mouseup', this.stopResize.bind(this));
                document.addEventListener('touchend', this.stopResize.bind(this));
                document.addEventListener('mouseleave', this.stopResize.bind(this)); // 브라우저 밖으로 나갔을 때
                
                window.addEventListener('resize', this.handleResize.bind(this));
            }

            startResize(e) {
                this.isResizing = true;
                document.body.style.cursor = this.isMobile ? 'row-resize' : 'col-resize';
                document.body.style.userSelect = 'none';
                
                // iframe에서 마우스 이벤트가 차단되지 않도록
                const iframes = document.querySelectorAll('iframe');
                iframes.forEach(iframe => {
                    iframe.style.pointerEvents = 'none';
                });
                
                e.preventDefault();
                e.stopPropagation();
            }

            resize(e) {
                if (!this.isResizing) return;

                e.preventDefault();
                e.stopPropagation();

                const clientX = e.clientX || (e.touches && e.touches[0] && e.touches[0].clientX);
                const clientY = e.clientY || (e.touches && e.touches[0] && e.touches[0].clientY);

                if (this.isMobile) {
                    // 모바일: 상하 분할
                    const containerRect = this.container.getBoundingClientRect();
                    const containerHeight = containerRect.height;
                    const relativeY = clientY - containerRect.top;
                    const newHeight = Math.max(200, Math.min(containerHeight - 300, relativeY));
                    const percentage = (newHeight / containerHeight) * 100;
                    this.sidebar.style.height = `${percentage}%`;
                } else {
                    // 데스크톱: 좌우 분할
                    const containerRect = this.container.getBoundingClientRect();
                    const containerWidth = containerRect.width;
                    const relativeX = clientX - containerRect.left;
                    const newWidth = Math.max(200, Math.min(containerWidth - 300, relativeX));
                    const percentage = (newWidth / containerWidth) * 100;
                    this.sidebar.style.width = `${percentage}%`;
                }
            }

            stopResize() {
                if (!this.isResizing) return;
                
                this.isResizing = false;
                document.body.style.cursor = '';
                document.body.style.userSelect = '';
                
                // iframe 포인터 이벤트 복원
                const iframes = document.querySelectorAll('iframe');
                iframes.forEach(iframe => {
                    iframe.style.pointerEvents = 'auto';
                });
            }

            handleResize() {
                const wasMobile = this.isMobile;
                this.isMobile = window.innerWidth <= 768;
                
                if (wasMobile !== this.isMobile) {
                    // 모바일/데스크톱 전환 시 스타일 초기화
                    if (this.isMobile) {
                        this.sidebar.style.width = '100%';
                        this.sidebar.style.height = '40%';
                    } else {
                        this.sidebar.style.width = '30%';
                        this.sidebar.style.height = 'auto';
                    }
                }
            }
        }

        // 페이지 로드 시 초기화
        document.addEventListener('DOMContentLoaded', () => {
            new TabManager();
            new ResizableLayout();
        });
    </script>
</body>
</html>
""";
}
