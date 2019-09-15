#!/bin/bash

minecraft_server_path=/srv/minecraft_server
minecraft_user=minecraft
minecraft_group=minecraft
memoryAllocs=8g
memoryAllocx=12g
server_jar=Hexxit.jar

MOD_SERVER_DOWNLOAD_URL="http://servers.technicpack.net/Technic/servers/hexxit/Hexxit_Server_v1.0.10.zip"

# add and update repos
while ! echo y | apt-get install -y software-properties-common; do
    sleep 10
    apt-get install -y software-properties-common
done

while ! echo y | apt-add-repository -y ppa:linuxuprising/java; do
    sleep 10
    apt-add-repository -y ppa:linuxuprising/java
done

while ! echo y | apt-get update; do
    sleep 10
    apt-get update
done

# Install Java12
echo oracle-java12-installer shared/accepted-oracle-license-v1-2 select true | /usr/bin/debconf-set-selections

while ! echo y | apt-get install -y oracle-java12-installer; do
    sleep 10
    apt-get install -y oracle-java12-installer
done

# create user and install folder
adduser --system --no-create-home --home $minecraft_server_path $minecraft_user
addgroup --system $minecraft_group
mkdir $minecraft_server_path
cd $minecraft_server_path

# download the server zip
wget -qO- -O tmp.zip "http://servers.technicpack.net/Technic/servers/hexxit/Hexxit_Server_v1.0.10.zip" && unzip tmp.zip && rm tmp.zip

# set permissions on install folder
chown -R $minecraft_user $minecraft_server_path

# create a service
touch /etc/systemd/system/minecraft-server.service
printf '[Unit]\nDescription=Minecraft Service\nAfter=rc-local.service\n' >> /etc/systemd/system/minecraft-server.service
printf '[Service]\nWorkingDirectory=%s\n' $minecraft_server_path >> /etc/systemd/system/minecraft-server.service
printf 'ExecStart=/usr/bin/java -Xms%s -Xmx%s -jar %s/%s nogui\n' $memoryAllocs $memoryAllocx $minecraft_server_path $server_jar >> /etc/systemd/system/minecraft-server.service
printf 'ExecReload=/bin/kill -HUP $MAINPID\nKillMode=process\nRestart=on-failure\n' >> /etc/systemd/system/minecraft-server.service
printf '[Install]\nWantedBy=multi-user.target\nAlias=minecraft-server.service' >> /etc/systemd/system/minecraft-server.service
chmod +x /etc/systemd/system/minecraft-server.service

systemctl start minecraft-server