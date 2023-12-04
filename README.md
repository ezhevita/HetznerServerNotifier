# HetznerServerNotifier
Sends notifications about new available servers from Hetzner to Telegram based on user-defined filters


## Configuration
1. Put your Telegram bot token in the `Bot:Token` section in the `appsettings.json` file
2. Put your Telegram user ID (can be revealed using [this bot](https://t.me/ShowJsonBot)) or any chat ID that you want to post messages to in the `Notifier:UsersToNotify` section
3. You can customize the delay between retrieving data from Hetzner (e.g. if they will decided to introduce cooldown in the future) by putting it in the `Notifier:DelayInSeconds` section
4. Customize filters to your needs; currently supported filters are `Cores`, `Price` (in EUR), `Frequency` (of the CPU), `Ram` (in GBs) and `Location`.
   - The first four of them are range filters, and can be configured by using `Min` and `Max` properties (if `Max` filter is 0, it is not applied).
   - The last one filters servers by location based on the allowed values in the configuration, currently supported values are `HEL` (Helsinki datacenter in Finland), `FSN` (Falkenstein datacenter in Germany) and `NBG` (Nuremberg datacenter in Germany).

## Deployment
Use Docker, i guess
