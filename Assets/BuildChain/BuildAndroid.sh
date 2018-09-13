#!/bin/bash

UNITY_VERSION="Unity_5_4_1p2"
JENKINS_ROOT="/Users/Ballena/Documents/juegospace"
WORKSPACE="${JENKINS_ROOT}/BCNJam"
USE_BUILD_UNITY_METHOD="BuildScript.BuildForAndroid"
LOG_FILE="${WORKSPACE}/android.txt"

SCENES="Assets/Project/Scenes/Game.unity"

ANDROID_KEYSTORE_PATH="${WORKSPACE}/projectRoot/android/amazon.keystore"
ANDROID_KEYSTORE_PASS="game jam barcelona"
ANDROID_KEYALIAS_NAME="playstore"
ANDROID_KEYALIAS_PASS="paolothomas"

BASE_VERSION="1.0"
SVN_REVISION=`svn info ${WORKSPACE}/projectRoot --show-item=revision | sed 's/ //g'`

echo "Building revision ${SVN_REVISION}"

if [ -d ${WORKSPACE}/builds/ ]
then
	echo "removing stuff in ${WORKSPACE}/builds/"
	rm -rf ${WORKSPACE}/builds/
fi



mkdir ${WORKSPACE}/builds/

echo "Starting Unity Build"

/Applications/${UNITY_VERSION}/Unity.app/Contents/MacOS/Unity \
	-batchmode \
	-projectPath ${WORKSPACE}/projectRoot/unity \
	-executeMethod ${USE_BUILD_UNITY_METHOD} \
	-quit \
	-logFile ${LOG_FILE} \
	-scenes ${SCENES} \
	-targetPlatform android \
	-bundleVersion ${BASE_VERSION}.${SVN_REVISION} \
	-androidBundleVersionCode ${SVN_REVISION} \
	-androidKeystore=${ANDROID_KEYSTORE_PATH} \
	-keystorePass=${ANDROID_KEYSTORE_PASS} \
	-keyaliasName=${ANDROID_KEYALIAS_NAME} \
	-keyaliasPass=${ANDROID_KEYALIAS_PASS} \
	-buildOutputPath ${WORKSPACE}/builds/android_${BASE_VERSION}.${SVN_REVISION}.apk
	
BUILD_SUCCESS=$?

if [ ${BUILD_SUCCESS} == 0 ]
then
	echo "Unity: Unity built successfully."
	exit 1
else
	echo "Unity: Unity failed with exit code ${BUILD_SUCCESS}"
	exit 0
fi

echo "Unity finished"