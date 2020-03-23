#!/bin/bash

minecraft_server_path=/srv/minecraft_blightfall
minecraft_user=minecraft-blightfall
minecraft_group=minecraft-blightfall
memoryAllocs=6g
memoryAllocx=8g
server_jar=minecraft_server.1.12.2
mod_server_uri="http://servers.technicpack.net/Technic/servers/blightfall/Blightfall_Server_v2.1.5.zip"

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
    wget -qO- -O tmp.zip "http://servers.technicpack.net/Technic/servers/blightfall/Blightfall_Server_v2.1.5.zip" && unzip tmp.zip && rm tmp.zip

    # set permissions on server folder
    chown -R minecraft $minecraft_server_path

    # create a service to run minecraft
    touch /etc/systemd/system/minecraft_blightfall.service
    printf '[Unit]\nDescription=Minecraft Service\nAfter=rc-local.service\n' >> /etc/systemd/system/minecraft_blightfall.service
    printf '[Service]\nWorkingDirectory=%s\n' $minecraft_server_path >> /etc/systemd/system/minecraft_blightfall.service
    printf 'ExecStart=/usr/bin/java -Xms%s -Xmx%s -jar %s/%s nogui\n' $memoryAllocs $memoryAllocx $minecraft_server_path $server_jar >> /etc/systemd/system/minecraft_blightfall.service
    printf 'ExecReload=/bin/kill -HUP $MAINPID\nKillMode=process\nRestart=on-failure\n' >> /etc/systemd/system/minecraft_blightfall.service
    printf '[Install]\nWantedBy=multi-user.target\nAlias=minecraft_blightfall.service' >> /etc/systemd/system/minecraft_blightfall.service
    chmod +x /etc/systemd/system/minecraft_blightfall.service

    systemctl enable minecraft_blightfall
    systemctl start minecraft_blightfall

fi
