#!/bin/bash

minecraft_server_path=/srv/minecraft_blightfall
<<<<<<< HEAD
minecraft_user=minecraft_blightfall
minecraft_group=minecraft_blightfall
minecraft_service_name=minecraft_blightfall
minecraft_service_file_path=/etc/systemd/system/minecraft_blightfall.service
memoryAllocs=8g
memoryAllocx=12g
server_jar=minecraft_server.1.7.10.jar
=======
minecraft_user=minecraft-blightfall
minecraft_group=minecraft-blightfall
memoryAllocs=6g
memoryAllocx=8g
server_jar=minecraft_server.1.12.2
>>>>>>> c9c4ce095873835a79fdb8cd1d3b6c02c893ed02
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
    wget -qO- -O tmp.zip $mod_server_uri && unzip tmp.zip && rm tmp.zip

    # set permissions on server folder
    chown -R $minecraft_user $minecraft_server_path

    # create a service to run minecraft
    touch $minecraft_service_file_path
    printf '[Unit]\nDescription=Minecraft Service\nAfter=rc-local.service\n' >> $minecraft_service_file_path
    printf '[Service]\nWorkingDirectory=%s\n' $minecraft_server_path >> $minecraft_service_file_path
    printf 'ExecStart=/usr/bin/java -Xms%s -Xmx%s -jar %s/%s nogui\n' $memoryAllocs $memoryAllocx $minecraft_server_path $server_jar >> $minecraft_service_file_path
    printf 'ExecReload=/bin/kill -HUP $MAINPID\nKillMode=process\nRestart=on-failure\n' >> $minecraft_service_file_path
    printf '[Install]\nWantedBy=multi-user.target\nAlias=%s.service' $minecraft_service_name >> $minecraft_service_file_path
    chmod +x $minecraft_service_file_path

    systemctl enable $minecraft_service_name
    systemctl start $minecraft_service_name

fi
