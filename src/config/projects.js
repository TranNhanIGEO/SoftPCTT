const projects = [
  [
    "EPSG:4326",
    "+title=WGS84(long/lat) +proj=longlat +datum=WGS84 +no_defs +type=crs",
  ],
  [
    "EPSG:3405",
    "+title=VN2000(x/y) +proj=utm +zone=48 +ellps=WGS84 +towgs84=-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278 +units=m +no_defs +type=crs",
  ],
  [
    "EPSG:9210",
    "+title=HCM2000(x/y) +proj=tmerc +lat_0=0 +lon_0=105.75 +k=0.9999 +x_0=500000 +y_0=0 +ellps=WGS84 +towgs84=-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278 +units=m +no_defs +type=crs",
  ],
];

export default projects;