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
$stmt = $faculties->getAllFaculties();

echo $stmt;