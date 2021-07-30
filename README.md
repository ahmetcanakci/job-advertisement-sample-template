# job-advertisement-sample-template
Job Advertisement Sample With Docker Microservice


<<<<<<< HEAD
How to Run this project on Docker

1- Install Docker your computer.
2- Run this docker command "docker-compose -f docker-compose.yml up"
3- Open browser adn write http://localhost:5001/swagger/index.html
4- Browser new tab then paste http://localhost:9200 you will see the elastic search _status

{
  "name" : "hzkR-2O",
  "cluster_name" : "docker-cluster",
  "cluster_uuid" : "CWP_C4_tRUOS1PSWID3z5Q",
  "version" : {
    "number" : "6.3.0",
    "build_flavor" : "default",
    "build_type" : "tar",
    "build_hash" : "424e937",
    "build_date" : "2018-06-11T23:38:03.357887Z",
    "build_snapshot" : false,
    "lucene_version" : "7.3.1",
    "minimum_wire_compatibility_version" : "5.6.0",
    "minimum_index_compatibility_version" : "5.0.0"
  },
  "tagline" : "You Know, for Search"
}

5- Browser new tab then paste http://localhost:5601 you will see the kibana url for elastic search. Project has elastic search create index and write some data features.
=======
# job-advertisement-sample-template
Job Advertisement Sample With Docker Microservice


How to Run this project on Docker

1- Install Docker your computer.

2- clone porject "git clone https://github.com/ahmetcanakci/job-advertisement-sample-template"

3- Open cmd in project folder then run this docker command "docker-compose -f docker-compose.yml up"


4- Open browser adn write http://localhost:5001/swagger/index.html

<img width="1723" alt="Screen Shot 2021-07-28 at 23 47 41" src="https://user-images.githubusercontent.com/7714272/127393769-d79ea124-2636-4670-a299-4caa9d6f147b.png">


5- Browser new tab then paste http://localhost:9200 you will see the elastic search _status

<img width="427" alt="Screen Shot 2021-07-28 at 23 46 01" src="https://user-images.githubusercontent.com/7714272/127393529-a9f2b073-6d53-400d-a04d-0cd423ff3865.png">


6- Then create employeer and create advert.  After create advert api create Elasticsearch index (advert)

7- Browser new tab then paste http://localhost:5601 you will see the kibana url for elastic search. Project has elastic search create index and write some data features.

<img width="1735" alt="Screen Shot 2021-07-28 at 23 52 20" src="https://user-images.githubusercontent.com/7714272/127394360-06b033f3-372e-4297-bc12-0e17d68fcf32.png">

>>>>>>> 8538fe2e2d6c8ec735a6e3066e85e3c64faea5be


