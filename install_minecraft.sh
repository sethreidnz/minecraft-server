#!/bin/bash

minecraft_server_path=/srv/minecraft_server
minecraft_user=minecraft
minecraft_group=minecraft
memoryAllocs=8g
memoryAllocx=12g
server_jar=Hexxit.jar
mod_server_uri="http://servers.technicpack.net/Technic/servers/hexxit/Hexxit_Server_v1.0.10.zip"

if [ ! -d "$minecraft_server_path" ]; then

    # create server folder
    mkdir $minecraft_server_path;
    cd $minecraft_server_path;

    apt-get update
    apt-get install -y software-properties-common;
    apt-get install -y unzip

    # create user and group for running the server
    adduser --system --no-create-home --home $minecraft_server_path $minecraft_user
    addgroup --system $minecraft_group

    # download the server zip
    wget -qO- -O tmp.zip $mod_server_uri && unzip tmp.zip && rm tmp.zip

    # set permissions on server folder
    chown -R $minecraft_user $minecraft_server_path

    # create a service to run minecraft
    touch /etc/systemd/system/minecraft-server.service
    printf '[Unit]\nDescription=Minecraft Service\nAfter=rc-local.service\n' >> /etc/systemd/system/minecraft-server.service
    printf '[Service]\nWorkingDirectory=%s\n' $minecraft_server_path >> /etc/systemd/system/minecraft-server.service
    printf 'ExecStart=/usr/bin/java -Xms%s -Xmx%s -jar %s/%s nogui\n' $memoryAllocs $memoryAllocx $minecraft_server_path $server_jar >> /etc/systemd/system/minecraft-server.service
    printf 'ExecReload=/bin/kill -HUP $MAINPID\nKillMode=process\nRestart=on-failure\n' >> /etc/systemd/system/minecraft-server.service
    printf '[Install]\nWantedBy=multi-user.target\nAlias=minecraft-server.service' >> /etc/systemd/system/minecraft-server.service
    chmod +x /etc/systemd/system/minecraft-server.service

    systemctl start minecraft-server
    systemctl enable minecraft-server

fi