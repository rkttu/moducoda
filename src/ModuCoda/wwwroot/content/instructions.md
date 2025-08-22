# Linux 기초 명령어 학습

## 1. 현재 위치 확인하기 (기초)

Linux에서 가장 기본적인 명령어인 `pwd`를 사용하여 현재 작업 디렉토리를 확인해보겠습니다.

```bash-copy+exec
pwd
```

예상 출력:

```text
/home/user
```

`pwd`는 "Print Working Directory"의 줄임말로, 현재 있는 폴더의 전체 경로를 보여줍니다.

---

## 2. 디렉토리 내용 확인하기 (기초)

`ls` 명령어로 현재 디렉토리에 있는 파일과 폴더들을 확인해보세요.

```bash
ls -la
```

예상 출력:

```text
total 24
drwxr-xr-x 3 user user 4096 Jan 15 10:30 .
drwxr-xr-x 3 root root 4096 Jan 15 10:25 ..
-rw-r--r-- 1 user user  220 Jan 15 10:25 .bash_logout
-rw-r--r-- 1 user user 3526 Jan 15 10:25 .bashrc
drwxr-xr-x 2 user user 4096 Jan 15 10:30 Documents
```

팁: `-la` 옵션은 숨겨진 파일(.)까지 자세한 정보와 함께 보여줍니다. `-l`은 자세한 정보, `-a`는 모든 파일을 의미합니다.

---

## 3. 새 디렉토리 만들기 (실습)

`mkdir` 명령어로 새로운 폴더를 만들어보겠습니다.

```bash
mkdir moducoda-practice
```

이제 `ls` 명령어로 새로 만든 디렉토리가 생성되었는지 확인해보세요:

```bash
ls
```

예상 출력:

```text
Documents  moducoda-practice
```

---

## 4. 디렉토리 이동하기 (실습)

`cd` 명령어로 방금 만든 디렉토리로 이동해보겠습니다.

```bash
cd moducoda-practice
```

이동했는지 확인하기 위해 다시 `pwd`를 실행해보세요:

```bash
pwd
```

예상 출력:

```text
/home/user/moducoda-practice
```

주의: Linux는 대소문자를 구분합니다. 디렉토리나 파일 이름을 정확히 입력해야 합니다.

---

다음에는 파일 생성, 편집, 그리고 권한 관리에 대해 학습하게 됩니다. 현재 학습한 기본 명령어들을 충분히 연습해보세요!
