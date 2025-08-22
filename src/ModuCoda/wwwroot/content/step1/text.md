# Step 1: Create a Pod

간단한 nginx Pod를 생성합니다.

```bash+exec
kubectl run web --image=nginx:1.25 --restart=Never
```

생성된 Pod를 확인합니다.

```bash+exec
kubectl get pod web -o wide
```

문제 해결 팁:

- 이미지 풀 이슈가 발생하면 네트워크나 레지스트리 접근 권한을 확인하세요.
- CrashLoopBackOff 상태일 경우 `kubectl logs web` 으로 로그를 확인하세요.
