#!/bin/bash

WORKSPACE="${JENKINS_ROOT}/BCNJam"

echo "SVN REVERT"

svn revert -R "${WORKSPACE}/projectRoot"

for file in `svn status|grep "^ *?"|sed -e 's/^ *? *//'`; do rm $file ; done

echo "SVN UPDATE"

svn update "${WORKSPACE}/projectRoot"

echo "BUILD"

(exec ${WORKSPACE}/projectRoot/unity/Assets/BuildChain/BuildAndroid.sh)

echo "Finished building"

BUILD_SUCCESS=$?

echo "Posting success to slack..."

if [ ${BUILD_SUCCESS} == 0 ]
then
	MESSAGE="Unity built successfully."
	bash $WORKSPACE/projectRoot/unity/Assets/BuildChain/ShellUtilities/PostToBuilds.sh "${MESSAGE}" "INFO"
	echo "Posted: ${MESSAGE}"
	exit 0
else
	MESSAGE="Unity failed with exit code ${BUILD_SUCCESS}"
	bash $WORKSPACE/projectRoot/unity/Assets/BuildChain/ShellUtilities/PostToBuilds.sh "${MESSAGE}" "WARNING"
	echo "Posted: ${MESSAGE}"
	exit 1
fi

echo "Finished"