#!/bin/sh

git filter-branch --env-filter '
OLD_EMAIL="Sage@d-172-25-98-87.dhcp.virginia.edu"
CORRECT_NAME="Zachary Danz"
CORRECT_EMAIL="zsd4yr@virginia.edu"
if [ "$GIT_COMMITTER_EMAIL" = "$OLD_EMAIL" ]
then
    export GIT_COMMITTER_NAME="$CORRECT_NAME"
    export GIT_COMMITTER_EMAIL="$CORRECT_EMAIL"
fi
if [ "$GIT_AUTHOR_EMAIL" = "$OLD_EMAIL" ]
then
    export GIT_AUTHOR_NAME="$CORRECT_NAME"
    export GIT_AUTHOR_EMAIL="$CORRECT_EMAIL"
fi
' --tag-name-filter cat -- --branches --tags
