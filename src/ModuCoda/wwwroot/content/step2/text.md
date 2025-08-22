# Step 2: Delete the Pod

앞서 생성한 Pod를 삭제합니다.

```bash+exec
kubectl delete pod web --wait=false
```

삭제 진행 상태를 확인합니다.

```bash+exec
kubectl get pod web -o json 2>/dev/null || echo "pod not found"
```

문제 해결 팁:

- finalizer나 evict 문제로 바로 삭제되지 않는다면 `--grace-period=0 --force`를 고려하세요.
