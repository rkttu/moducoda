#!/usr/bin/env bash
set -euo pipefail

# Verify pod named 'web' exists and is Running
if ! kubectl get pod web >/dev/null 2>&1; then
  echo "❌ Pod 'web'가 존재하지 않습니다. 'kubectl run web --image=nginx:1.25 --restart=Never'를 실행했는지 확인하세요."
  exit 1
fi

phase=$(kubectl get pod web -o jsonpath='{.status.phase}' 2>/dev/null || echo "")
if [[ "$phase" != "Running" ]]; then
  echo "❌ Pod 'web' 상태가 Running이 아닙니다. 현재 상태: $phase"
  kubectl get pod web -o wide || true
  exit 1
fi

echo "✅ 검증 성공: Pod 'web'이 Running 상태입니다."
