<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

// include database and object files
include_once 'connection.php';
include_once 'faculties.php';

// instantiate database and product object
$connection = new Connection();
$db = $connection->getConnstring();

// initialize object
$faculties = new Faculties($db);

// query products
//$id=isset($_GET['id']) ? $_GET['id'] : die();
if(isset($_GET['fakultetId'])){
  $fakultetId=$_GET['fakultetId'];
  $stmt = $faculties->getFaculties($fakultetId);

  echo $stmt;
  //return $stmt;

}else {
  http_response_code(400);
  echo json_encode(array("message" => "Fakultet ne postoji."));
  //echo "404";
}
