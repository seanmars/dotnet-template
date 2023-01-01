# Docker

此專案的目的是將 `ASP.NET Core` 的專案利用 `docker` 來建立並且執行。

- Dockerfile -> 主要建立 docker image 的文件，流程為使用 sdk-image 來建立專案檔案，並將建立出來的專案檔案複製到 runtime-image 來製作出最後的 docker image。
- docker 資料夾 -> 為預設的 `container` 執行環境，會將部分所需的空間使用 `mount` 來達到永久儲存的目的，這部分可以改用 docker 提供的 `volume` 來做。
- docker-compose.yaml -> 主要的執行點，直接利用 `docker` 的 `docker-compose` 來建立起 `container` 並執行；這部分也可以進一步將建立 `image` 直接寫在 `yaml` 檔案中，但這邊沒有如此做主要是想將建立以及執行分開。
