#!/bin/bash

server="$1"

echo publish to server $server

cd src/HtwLessonPlan/

# --runtime active prevents that only one runtime will be added, this keeps the package smaller
dnu publish --runtime active --no-source

cd bin/
#pack
tar -zcvf output.tar.gz output/

#push
scp output.tar.gz $server:~/deploy

#unpack at server
ssh $server 'cd deploy; tar -zxf output.tar.gz'

#cleanup
rm -rf bin/output output.tar.gz




