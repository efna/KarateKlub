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

if (isset($_GET["FacultyId"])){
    $fakultetId=$_GET["FacultyId"];
}
if(is_numeric($fakultetId)){

    $faculties->fakultetId=$fakultetId;
    $stmt = $faculties->deleteFaculties($fakultetId);

    if($stmt){
        http_response_code(200);
        // tell the user
        echo json_encode(array("message" => "Uspješno ste obrisali fakultet!"));
    }
    else{

        // set response code - 503 service unavailable
        http_response_code(503);

        // tell the user
        echo json_encode(array("message" => "Fakultet nije moguće obrisati."));
    }
}
else{
    // set response code - 400 bad request
    http_response_code(400);

    // tell the user
    echo json_encode(array("message" => "ID fakulteta koji ste proslijedili nije broj!"));
}
