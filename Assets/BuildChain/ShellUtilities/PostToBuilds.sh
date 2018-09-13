#!/bin/bash

function post_to_slack () {
  # format message as a code block ```${msg}```
  # SLACK_MESSAGE="\`\`\`$1\`\`\`"
  SLACK_MESSAGE="$1"
  SLACK_URL=https://hooks.slack.com/services/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
 
  case "$2" in
    INFO)
      SLACK_ICON=':slack:'
      ;;
    WARNING)
      SLACK_ICON=':warning:'
      ;;
    ERROR)
      SLACK_ICON=':bangbang:'
      ;;
    *)
      SLACK_ICON=':slack:'
      ;;
  esac
  
  echo "Posting to slack ${SLACK_MESSAGE}"
 
  curl -X POST --data "payload={\"text\": \"${SLACK_ICON} ${SLACK_MESSAGE}\"}" ${SLACK_URL}
}

post_to_slack "$1" "$2"
exit 0

