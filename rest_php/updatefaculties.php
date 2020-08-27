<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Methods: PUT");
header("Access-Control-Max-Age: 3600");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

// include database and object files
include_once 'connection.php';
include_once 'faculties.php';

// instantiate database and product object
$connection = new Connection();
$db = $connection->getConnstring();

// initialize object
$faculties = new Faculties($db);

if (isset($_GET["FacultyId"])){
    $fakultetId=$_GET["FacultyId"];
}

// get posted data
$data = json_decode(file_get_contents("php://input"));

// make sure data is not empty and id is number
if (!is_numeric($fakultetId)){
  http_response_code(400);
  // tell the user
  echo json_encode(array("message" => "FakultetID nije broj."));
}
else{

  if( !empty($fakultetId) &&
      !empty($data->Name)
  )
  {

      // set product property values
      $faculties->fakultetId=$fakultetId;
      $faculties->Name=$data->Name;
  	  
      // create the product
      if($faculties->updateFaculties($fakultetId)){

          // set response code - 201 created
          http_response_code(201);

          // tell the user
          echo json_encode(array("message" => "Uspjesno ste izmijenili podatke o fakultetu."));
      }

      // if unable to create the product, tell the user
      else{

          // set response code - 503 service unavailable
          http_response_code(503);

          // tell the user
          echo json_encode(array("message" => "Nije moguce izmijeniti podatke o fakultetu."));
      }
  }

  // tell the user data is incomplete
  else{
      // set response code - 400 bad request
      http_response_code(400);

      // tell the user
      echo json_encode(array("message" => "Niste unijeli sve podatke. Pokusajte ponovo!"));
  }
}
