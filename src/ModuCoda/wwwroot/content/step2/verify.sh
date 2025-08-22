#!/usr/bin/env bash
set -euo pipefail

# Verify pod named 'web' does not exist
if kubectl get pod web >/dev/null 2>&1; then
  # Check if terminating
  phase=$(kubectl get pod web -o jsonpath='{.status.phase}' 2>/dev/null || echo "")
  if [[ "$phase" == "" ]]; then
    echo "❌ Pod 'web'가 아직 삭제 중이거나 상태 조회에 실패했습니다. 잠시 후 다시 시도하세요."
  else
    echo "❌ Pod 'web'가 아직 존재합니다. 현재 상태: $phase"
  fi
  exit 1
fi

echo "✅ 검증 성공: Pod 'web'이 삭제되었습니다."
