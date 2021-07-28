# job-advertisement-sample-template
Job Advertisement Sample With Docker Microservice


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


