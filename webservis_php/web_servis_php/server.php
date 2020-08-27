<?php

if(!extension_loaded("soap")){
  dl("php_soap.dll");
}

ini_set("soap.wsdl_cache_enabled","0");

$server = new SoapServer("profesori.wsdl");

function listProfesori($departament_id){

  $departamentID = $departament_id;
  $resultData= array();

  $conn = mysqli_connect("localhost","root","Dalia0909");

  if($conn) {
    $result = mysqli_select_db($conn,"university");
    if(!$result){
      throw new SoapFault("Server","Nije se spojio na bazu.");
    }
          
    $sql = "SELECT p.Name,d.Name,t.Type,f.Name
FROM professors as p JOIN departaments as d
on p.Departaments_DepartamentId=d.DepartamentId
JOIN professors_titles AS pt
ON pt.Professors_ProfessorId=p.ProfessorId JOIN titles as t
on t.TitleId=pt.Titles_TitleId JOIN faculties as f
on f.FacultyId=d.Faculties_FacultyId
where p.Departaments_DepartamentId=$departament_id";


    $result2 = mysqli_query($conn, $sql);
    if(!$result2){
      throw new SoapFault("Server","Nije dohvatio rezultat.");
    }
    
    while($row = mysqli_fetch_array($result2)) {
        $resultData[]=$row;
    }
    
    return $resultData;
    mysqli_close($conn);
  }
  else {
  throw new SoapFault("Server","Nije se spojio na server baze.");
  }
}

$server->AddFunction("listProfesori");
$server->handle();
?>
