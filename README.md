# job-advertisement-sample-template
Job Advertisement Sample With Docker Microservice


# job-advertisement-sample-template
Job Advertisement Sample With Docker Microservice


How to Run this project on Docker

1- Install Docker your computer.

2- clone porject "git clone https://github.com/ahmetcanakci/job-advertisement-sample-template"

3- run this command terminal or command window "docker network create esnetwork"

4- Open cmd in project folder then run this docker command "docker-compose -f docker-compose.yml up"


5- Open browser adn write http://localhost:5001/swagger/index.html

<img width="1723" alt="Screen Shot 2021-07-28 at 23 47 41" src="https://user-images.githubusercontent.com/7714272/127393769-d79ea124-2636-4670-a299-4caa9d6f147b.png">


6- Browser new tab then paste http://localhost:9200 you will see the elastic search _status

<img width="427" alt="Screen Shot 2021-07-28 at 23 46 01" src="https://user-images.githubusercontent.com/7714272/127393529-a9f2b073-6d53-400d-a04d-0cd423ff3865.png">


7- Then create employeer and create advert.  After create advert api create Elasticsearch index (advert)

8- Browser new tab then paste http://localhost:5601 you will see the kibana url for elastic search. Project has elastic search create index and write some data features.

<img width="1735" alt="Screen Shot 2021-07-28 at 23 52 20" src="https://user-images.githubusercontent.com/7714272/127394360-06b033f3-372e-4297-bc12-0e17d68fcf32.png">

9- If you call search api method http://localhost:5001/adverts/search?date=2021-08-14 you may get "TypeError: Failed to fetch"
error. Its related to CORS policy. If you get this error please open swagger ui  chrome incognito mode.

